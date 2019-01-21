const SRD = window["storm-react-diagrams"];

class TaskDiagram extends React.Component {
    constructor(props) {
        super(props);

        const diagramEngine = new SRD.DiagramEngine();

        this.props.models.ports.map(p => diagramEngine.registerPortFactory(new BasePortFactory(p)));
        this.props.models.links.map(l => diagramEngine.registerLinkFactory(new BaseLinkFactory(l)));
        diagramEngine.registerNodeFactory(new BaseFunctionFactory(this.props.models.functions.all));

        const graphModel = new BaseGraphModel(this.props.graph.json, this.props.graph.mode, diagramEngine);

        diagramEngine.setDiagramModel(graphModel);

        this.diagramEngine = diagramEngine;

        window.diagram = this;
    }

    requestWithSerializedGraphBody = () => {
        console.log(this.diagramEngine.diagramModel.serializeDiagram());
        return {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "X-TB-Token": this.props.secureToken
            },
            body: JSON.stringify(this.diagramEngine.diagramModel.serializeDiagram())
        };
    }

    saveTask = () => this.postTask(this.props.endpoints.save, this.requestWithSerializedGraphBody(), "saveTask");

    runTask = () => this.postTask(this.props.endpoints.run, this.requestWithSerializedGraphBody(), "runTask");

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

    dropFunction = (signature, mousePoint) => {
        const node = this.diagramEngine
            .getNodeFactory("function")
            .getNewInstance(null, signature, true, mousePoint);

        this.diagramEngine
            .getDiagramModel()
            .addNode(node);

        this.refs.diagram.forceUpdate();
    }

    render() {
        let saveButton;

        if (this.props.graph.mode !== "sandbox") {
            saveButton = (
                <DiagramButton
                    text="Save Task"
                    iconClass="icon-arrow-down-line"
                    onClick={() => this.saveTask()}
                />
            );
        }

        return (
            <div className="task-builder-area">
                <div className="task-builder-buttons">
                    {saveButton}
                    <DiagramButton
                        text="Run Task"
                        iconClass="icon-triangle-right"
                        onClick={() => this.runTask()}
                    />
                </div>
                <div className="task-builder-row">
                    <FunctionTray
                        functions={
                            _.intersectionWith(
                                this.props.models.functions.all,
                                this.props.models.functions.authorized,
                                (a, b) => a.typeIdentifier === b
                            )
                        }
                    />
                    <div className="task-builder-diagram-wrapper"
                        onDrop={e => this.dropFunction(e.dataTransfer.getData("functionSignature"), this.diagramEngine.getRelativeMousePoint(e))}
                        onDragOver={e => { e.preventDefault(); }}
                    >
                        <DiagramToast ref="toast" />
                        <SRD.DiagramWidget className="task-builder-diagram"
                            ref="diagram"
                            diagramEngine={this.diagramEngine}
                            maxNumberPointsPerLink={0}
                            allowLooseLinks={false}
                        />
                    </div>
                </div>
            </div>
        );
    }
}