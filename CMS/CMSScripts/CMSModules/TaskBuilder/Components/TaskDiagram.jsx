const SRD = window["storm-react-diagrams"];

class TaskDiagram extends React.Component {
    constructor(props) {
        super(props);

        diagramEngine = new SRD.DiagramEngine();
        taskBuilderDataSource = new TaskBuilderDataSource(props);

        diagramEngine.registerNodeFactory(new BaseFunctionFactory());
        taskBuilderDataSource.models.ports.map(p => diagramEngine.registerPortFactory(new BasePortFactory(p)));
        taskBuilderDataSource.models.links.map(l => diagramEngine.registerLinkFactory(new BaseLinkFactory(l)));

        const graphModel = new BaseGraphModel(taskBuilderDataSource.graph, diagramEngine);

        diagramEngine.setDiagramModel(graphModel);

        if (taskBuilderDataSource.graph.mode !== "sandbox") {
            this.saveButton = (
                <DiagramButton
                    text="Save Task"
                    iconClass="icon-arrow-down-line"
                    onClick={() => this.saveTask()}
                />
            );
        }
    }

    saveButton;

    requestWithSerializedGraphBody = () => {
        console.log(diagramEngine.diagramModel.serializeDiagram());
        return {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "X-TB-Token": taskBuilderDataSource.secureToken
            },
            body: JSON.stringify(diagramEngine.diagramModel.serializeDiagram())
        };
    }

    saveTask = () => this.postTask(taskBuilderDataSource.endpoints.save, this.requestWithSerializedGraphBody(), "saveTask");

    runTask = () => this.postTask(taskBuilderDataSource.endpoints.run, this.requestWithSerializedGraphBody(), "runTask");

    postTask = (endpoint, request, actionName) => {
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

    showToast = (data) => {
        this.refs.toast.setState({
            show: true,
            result: data.result,
            message: data.message
        });
    }

    dropFunction = (typeGuid, mousePoint) => {
        const node = diagramEngine
            .getNodeFactory("function")
            .getNewInstance(null, typeGuid, mousePoint);

        diagramEngine
            .getDiagramModel()
            .addNode(node);

        this.refs.diagram.forceUpdate();
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
                        onDrop={e => this.dropFunction(e.dataTransfer.getData("typeGuid"), diagramEngine.getRelativeMousePoint(e))}
                        onDragOver={e => { e.preventDefault(); }}
                    >
                        <DiagramToast ref="toast" />
                        <SRD.DiagramWidget
                            className="task-builder-diagram"
                            ref="diagram"
                            diagramEngine={diagramEngine}
                            maxNumberPointsPerLink={0}
                            allowLooseLinks={false}
                        />
                    </div>
                </div>
            </div>
        );
    }
}