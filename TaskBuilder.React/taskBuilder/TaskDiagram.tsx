import * as React from "react";
import * as _ from "lodash";
import { DiagramWidget } from "storm-react-diagrams";

import { ITaskBuilderProps } from "./TaskBuilder";
import { taskBuilderDataSource } from "./TaskBuilderDataSource";
import { TaskGraphMode } from "../models/graph/TaskGraphMode";
import { DiagramButton } from "./DiagramButton";
import { DiagramToast } from "./DiagramToast";
import { FunctionFactory } from "../graph/factories/FunctionFactory";
import { PortFactory } from "../graph/factories/PortFactory";
import { LinkFactory } from "../graph/factories/LinkFactory";
import { GraphModel } from "../graph/models/GraphModel";
import { FunctionTray } from "../functionTray/FunctionTray";
import { GraphEngine } from "../graph/GraphEngine";

export let graphEngine: GraphEngine;

export class TaskDiagram extends React.Component<ITaskBuilderProps> {
    private toast: DiagramToast | null;
    private saveButton: JSX.Element;
    private diagram: DiagramWidget | null;

    constructor(props: ITaskBuilderProps) {
        super(props);

        graphEngine = new GraphEngine();
        taskBuilderDataSource.load(props);

        graphEngine.registerNodeFactory(new FunctionFactory());
        taskBuilderDataSource.models.ports.map(p => graphEngine.registerPortFactory(new PortFactory(p)));
        taskBuilderDataSource.models.links.map(l => graphEngine.registerLinkFactory(new LinkFactory(l)));

        const graphModel = new GraphModel(taskBuilderDataSource.graph, graphEngine);

        graphEngine.setDiagramModel(graphModel);

        if (taskBuilderDataSource.graph.mode !== TaskGraphMode.sandbox) {
            this.saveButton = (
                <DiagramButton
                    text="Save Task"
                    iconClass="icon-arrow-down-line"
                    onClick={() => this.saveTask()}
                />
            );
        }
    }

    private requestWithSerializedGraphBody = (): RequestInit => {
        console.log(graphEngine.graphModel.serializeGraph());

        return {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "X-TB-Token": taskBuilderDataSource.secureToken
            },
            body: JSON.stringify(graphEngine.graphModel.serializeGraph())
        };
    }

    private saveTask = () => this.postTask(taskBuilderDataSource.endpoints.save, this.requestWithSerializedGraphBody(), "saveTask");

    private runTask = () => this.postTask(taskBuilderDataSource.endpoints.run, this.requestWithSerializedGraphBody(), "runTask");

    private postTask = (endpoint: string, request: RequestInit, actionName: string) => {
        fetch(endpoint, request)
            .then(response => {
                if (response.status !== 200) {
                    console.log(`${actionName} responded ${response.status}`, response);
                    return;
                }

                response.json().then(data => this.showToast(data));
            })
            .catch(err => {
                console.log(`${actionName} errored ${err.message}`, err);
            });
    }

    showToast = (data: any) => {
        if (this.toast) {
            this.toast.setState({
                show: true,
                result: data.result,
                message: data.message
            });
        }
    }

    dropFunction = (typeGuid: string, mousePoint: {x:number, y: number}) => {
        const node = graphEngine
            .getNodeFactory("function")
            .getNewInstance({
                node: null,
                typeGuid: typeGuid,
                mousePoint: mousePoint
            });

        graphEngine
            .getDiagramModel()
            .addNode(node);

        if (this.diagram) {
            this.diagram.forceUpdate();
        }
    }

    render() {
        return (
            <div className="task-builder-area">
                <div className="task-builder-buttons">
                    {this.saveButton}
                    <DiagramButton
                        text="Run Task"
                        iconClass="icon-triangle-right"
                        onClick={() => this.runTask()}
                    />
                </div>
                <div className="task-builder-row">
                    <FunctionTray />
                    <div
                        className="task-builder-diagram-wrapper"
                        onDrop={e => this.dropFunction(e.dataTransfer.getData("typeGuid"), graphEngine.getRelativeMousePoint(e))}
                        onDragOver={e => { e.preventDefault(); }}
                    >
                        <DiagramToast ref={e => this.toast = e} />
                        <DiagramWidget
                            className="task-builder-diagram"
                            ref={e => this.diagram = e}
                            diagramEngine={graphEngine}
                            maxNumberPointsPerLink={0}
                            allowLooseLinks={false}
                        />
                    </div>
                </div>
            </div>
        );
    }
}