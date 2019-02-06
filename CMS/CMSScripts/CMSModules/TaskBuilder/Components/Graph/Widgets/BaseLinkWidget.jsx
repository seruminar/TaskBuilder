class BaseLinkWidget extends SRD.BaseWidget {
    // DOM references to the label and paths (if label is given), used to calculate dynamic positioning
    refLabels = {};
    refPaths = [];

    constructor(props) {
        super("srd-default-link", props);

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

    addPointToLink = (event, index) => {
        if (
            !event.shiftKey &&
            !this.props.diagramEngine.isModelLocked(this.props.link) &&
            this.props.link.points.length - 1 <= this.props.diagramEngine.getMaxNumberPointsPerLink()
        ) {
            const point = new PointModel(this.props.link, this.props.diagramEngine.getRelativeMousePoint(event));
            point.setSelected(true);
            this.forceUpdate();
            this.props.link.addPoint(point, index);
            this.props.pointAdded(point, event);
        }
    };

    generatePoint(pointIndex) {
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

    generateLabel(label) {
        const canvas = this.props.diagramEngine.canvas;
        return (
            <foreignObject
                key={label.id}
                className={this.bem("__label")}
                width={canvas.offsetWidth}
                height={canvas.offsetHeight}
            >
                <div ref={ref => this.refLabels[label.id] = ref}>
                    {this.props.diagramEngine
                        .getFactoryForLabel(label)
                        .generateReactWidget(this.props.diagramEngine, label)}
                </div>
            </foreignObject>
        );
    }

    generateLink(path, extraProps, id) {
        const props = this.props;

        const Bottom = React.cloneElement(
            props.diagramEngine
                .getFactoryForLink(props.link)
                .generateLinkSegment(
                    props.link,
                    this,
                    this.state.selected || props.link.isSelected(),
                    path
                ),
            {
                ref: ref => ref && this.refPaths.push(ref)
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
            ref: null,
            "data-linkid": this.props.link.getID(),
            strokeOpacity: this.state.selected ? 0.1 : 0,
            strokeWidth: 20,
            onContextMenu: () => {
                if (!this.props.diagramEngine.isModelLocked(this.props.link)) {
                    event.preventDefault();
                    this.props.link.remove();
                }
            }
        });

        return (
            <g key={"link-" + id}>
                {Bottom}
                {Top}
            </g>
        );
    }

    findPathAndRelativePositionToRenderLabel = (index) => {
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
    };

    calculateLabelPosition = (label, index) => {
        if (!this.refLabels[label.id]) {
            // no label? nothing to do here
            return;
        }

        const { path, position } = this.findPathAndRelativePositionToRenderLabel(index);

        const labelDimensions = {
            width: this.refLabels[label.id].offsetWidth,
            height: this.refLabels[label.id].offsetHeight
        };

        const pathCentre = path.getPointAtLength(position);

        const labelCoordinates = {
            x: pathCentre.x - labelDimensions.width / 2 + label.offsetX,
            y: pathCentre.y - labelDimensions.height / 2 + label.offsetY
        };
        this.refLabels[label.id].setAttribute(
            "style",
            `transform: translate(${labelCoordinates.x}px, ${labelCoordinates.y}px);`
        );
    };

    generateCurvePath = (firstPoint, lastPoint, curvyness) => {
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
            const pointLeft = points[0];
            const pointRight = points[1];

            paths.push(
                this.generateLink(
                    this.generateCurvePath(pointLeft, pointRight, this.props.link.curvyness),
                    {
                        onMouseDown: event => this.addPointToLink(event, 1)
                    },
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
                        SRD.Toolkit.generateLinePath(points[j], points[j + 1]),
                        {
                            "data-linkid": this.props.link.id,
                            "data-point": j,
                            onMouseDown: event => this.addPointToLink(event, j + 1)
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