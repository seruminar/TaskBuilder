import * as React from "react";
import * as _ from "lodash";

import { BaseWidget, DefaultLinkState, PointModel, LabelModel, BaseWidgetProps, Toolkit } from "storm-react-diagrams";
import { LinkFactory } from "../../factories/LinkFactory";
import { BaseLinkModel } from "../../models/link/BaseLinkModel";
import { GraphEngine } from "../../GraphEngine";

export interface LinkProps extends BaseWidgetProps {
    color?: string;
    width?: number;
    smooth?: boolean;
    link: BaseLinkModel;
    diagramEngine: GraphEngine;
    pointAdded?: (point: PointModel, event: MouseEvent) => any;
}

export class BaseLinkWidget extends BaseWidget<LinkProps, DefaultLinkState> {
    // DOM references to the label and paths (if label is given), used to calculate dynamic positioning
    refLabels: { [id: string]: HTMLElement | null };
    refPaths: SVGPathElement[];

    constructor(props: LinkProps) {
        super("srd-default-link", props);

        this.refLabels = {};
        this.refPaths = [];

        this.state = {
            selected: false
        };
    }

    calculateAllLabelPosition() {
        _.forEach(this.props.link.labels, (label, index) => {
            this.calculateLabelPosition(label, index + 1);
        });
    }

    componentDidUpdate() {
        if (this.props.link.labels.length > 0) {
            window.requestAnimationFrame(this.calculateAllLabelPosition.bind(this));
        }
    }

    componentDidMount() {
        if (this.props.link.labels.length > 0) {
            window.requestAnimationFrame(this.calculateAllLabelPosition.bind(this));
        }
    }

    generatePoint(pointIndex: number): JSX.Element {
        const x = this.props.link.points[pointIndex].x;
        const y = this.props.link.points[pointIndex].y;

        return (
            <g key={"point-" + this.props.link.points[pointIndex].id}>
                <circle
                    cx={x}
                    cy={y}
                    r={5}
                    className={
                        "point " +
                        this.bem("__point") +
                        (this.props.link.points[pointIndex].isSelected() ? this.bem("--point-selected") : "")
                    }
                />
                <circle
                    onMouseLeave={() => {
                        this.setState({ selected: false });
                    }}
                    onMouseEnter={() => {
                        this.setState({ selected: true });
                    }}
                    data-id={this.props.link.points[pointIndex].id}
                    data-linkid={this.props.link.id}
                    cx={x}
                    cy={y}
                    r={15}
                    opacity={0}
                    className={"point " + this.bem("__point")}
                />
            </g>
        );
    }

    generateLabel(label: LabelModel) {
        const { diagramEngine } = this.props;

        const canvas = diagramEngine.canvas as HTMLElement;

        const labelFactory = diagramEngine
            .getFactoryForLabel(label);

        let widget: JSX.Element | null = null;

        if (labelFactory) {
            widget = labelFactory
                .generateReactWidget(diagramEngine, label);
        }

        return (
            <foreignObject
                key={label.id}
                className={this.bem("__label")}
                width={canvas.offsetWidth}
                height={canvas.offsetHeight}
            >
                <div ref={ref => this.refLabels[label.id] = ref}>
                    {widget}
                </div>
            </foreignObject>
        );
    }

    generateLink(path: string, extraProps: any, id: string | number): JSX.Element {
        const { diagramEngine, link } = this.props;

        const linkFactory = diagramEngine
            .getFactoryForLink(link) as LinkFactory;

        if (linkFactory) {
            const segment = linkFactory.generateLinkSegment(
                link,
                this,
                this.state.selected || link.isSelected(),
                path
            );

            const Bottom = React.cloneElement(segment,
                {
                    ref: (ref: SVGPathElement) => ref && this.refPaths.push(ref)
                }
            );

            const Top = React.cloneElement(Bottom, {
                ...extraProps,
                strokeLinecap: "round",
                onMouseLeave: () => {
                    this.setState({ selected: false });
                },
                onMouseEnter: () => {
                    this.setState({ selected: true });
                },
                "data-linkid": link.getID(),
                strokeOpacity: this.state.selected ? 0.1 : 0,
                strokeWidth: link.width * 5
            });

            return (
                <g key={"link-" + id} >
                    {Bottom}
                    {Top}
                </g>
            );
        }

        throw new Error("No link factory!");
    }

    findPathAndRelativePositionToRenderLabel = (index: number): { path: SVGPathElement; position: number } => {
        // an array to hold all path lengths, making sure we hit the DOM only once to fetch this information
        const lengths = this.refPaths.map(path => path.getTotalLength());

        // calculate the point where we want to display the label
        let labelPosition =
            lengths.reduce((previousValue, currentValue) => previousValue + currentValue, 0) *
            (index / (this.props.link.labels.length + 1));

        // find the path where the label will be rendered and calculate the relative position
        let pathIndex = 0;
        while (pathIndex < this.refPaths.length) {
            if (labelPosition - lengths[pathIndex] < 0) {
                return {
                    path: this.refPaths[pathIndex],
                    position: labelPosition
                };
            }

            // keep searching
            labelPosition -= lengths[pathIndex];
            pathIndex++;
        }

        throw new Error("No paths to search!");
    };

    calculateLabelPosition = (label: LabelModel, index: number) => {
        const foundLabel = this.refLabels[label.id];

        if (foundLabel) {
            const { path, position } = this.findPathAndRelativePositionToRenderLabel(index);

            const labelDimensions = {
                width: foundLabel.offsetWidth,
                height: foundLabel.offsetHeight
            };

            const pathCentre = path.getPointAtLength(position);

            const labelCoordinates = {
                x: pathCentre.x - labelDimensions.width / 2 + label.offsetX,
                y: pathCentre.y - labelDimensions.height / 2 + label.offsetY
            };

            foundLabel.style.transform = `translate(${labelCoordinates.x}px, ${labelCoordinates.y}px);`;
        }
    };

    generateCurvePath = (firstPoint: PointModel, lastPoint: PointModel, curvyness: number) => {
        const totalDistance = Math.abs(firstPoint.x - lastPoint.x) + Math.abs(firstPoint.y - lastPoint.y);

        const curvy = curvyness * totalDistance / 200;

        return `M${firstPoint.x},${firstPoint.y} C ${firstPoint.x + curvy},${firstPoint.y}
                ${lastPoint.x - curvy},${lastPoint.y} ${lastPoint.x},${lastPoint.y}`;
    }

    render() {
        const { diagramEngine } = this.props;
        if (!diagramEngine.nodesRendered) {
            return null;
        }

        //ensure id is present for all points on the path
        const points = this.props.link.points;
        let paths = [];

        if (points.length === 2) {
            const pointLeft = points[1].x > points[0].x ? points[0] : points[1];
            const pointRight = points[1].x > points[0].x ? points[1] : points[0];

            paths.push(
                this.generateLink(
                    this.generateCurvePath(pointLeft, pointRight, this.props.link.curvyness),
                    null,
                    "0"
                )
            );

            // draw the link as dangeling
            if (this.props.link.targetPort === null) {
                paths.push(this.generatePoint(1));
            }
        } else {
            //draw the multiple anchors and complex line instead
            for (let j = 0; j < points.length - 1; j++) {
                paths.push(
                    this.generateLink(
                        Toolkit.generateLinePath(points[j], points[j + 1]),
                        {
                            "data-linkid": this.props.link.id,
                            "data-point": j
                        },
                        j
                    )
                );
            }

            //render the circles
            for (var i = 1; i < points.length - 1; i++) {
                paths.push(this.generatePoint(i));
            }

            if (this.props.link.targetPort === null) {
                paths.push(this.generatePoint(points.length - 1));
            }
        }

        this.refPaths = [];
        return (
            <g {...this.getProps()}>
                {paths}
                {_.map(this.props.link.labels, labelModel => {
                    return this.generateLabel(labelModel);
                })}
            </g>
        );
    }
}