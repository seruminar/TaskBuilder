const SRD = window["storm-react-diagrams"];

class TaskDiagram extends React.Component {
    constructor(props) {
        super(props);

        let engine = new SRD.DiagramEngine();

        // Register default port factory
        engine.registerPortFactory(new SRD.DefaultPortFactory());

        // Register two link types (for now, only default links can be made)
        engine.registerLinkFactory(new BaseLinkFactory("caller"));
        engine.registerLinkFactory(new BaseLinkFactory("default"));

        // Register factories from initialization code
        this.props.functions.map((f) => engine.registerNodeFactory(new BaseNodeFactory(f)));

        // Deserialize from database
        const graphModel = new SRD.DiagramModel();
        graphModel.deSerializeDiagram(JSON.parse(this.props.graph), engine);
        graphModel.setGridSize(10);
        graphModel.setZoomLevel(100);

        engine.setDiagramModel(graphModel);

        this.engine = engine;
    }

    ProcessGraph(e, type) {
        let serialized = JSON.stringify(this.engine.diagramModel.serializeDiagram());
        let toast = this.refs.toast;
        let endpoint;

        switch (type) {
            case "save":
                endpoint = "SaveTask";
                break;
            case "run":
                endpoint = "RunTask";
                break;
            default:
        }

        let request = {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json; charset=utf-8",
                "X-TB-Token": this.props.secureToken
            },
            body: serialized
        };

        fetch("/Kentico11_hf_TaskBuilder/taskbuilder/Tasks/" + endpoint, request)
            .then(function (response) {
                if (response.status !== 200) {
                    console.log("Call to " + type + " responded: " + response.status);
                    return;
                }

                response.json().then(function (data) {
                    switch (data.result) {
                        case "savesuccess":
                            toast.setState({
                                show: true,
                                message: "Save successful."
                            });
                            break;
                        default:
                    }
                });
            })
            .catch(function (err) {
                console.log('SaveGraph', err);
            });
    }

    DropFunction(e) {
        const functionModel = JSON.parse(e.dataTransfer.getData("functionModel"));

        const nodeFactory = this.engine.getNodeFactory(functionModel.name);

        const node = nodeFactory.getNewInstance(null, true);

        const points = this.engine.getRelativeMousePoint(e);

        node.x = points.x;
        node.y = points.y;

        this.engine
            .getDiagramModel()
            .addNode(node);

        this.refs.diagram.forceUpdate();
    }

    render() {
        return (
            <div className="task-builder-area">
                <div className="task-builder-buttons">
                    <DiagramButton text="Save Graph"
                        onClick={e => this.ProcessGraph(e, "save")}
                    />
                    <DiagramButton text="Run Graph"
                        onClick={e => this.ProcessGraph(e, "run")}
                    />
                </div>
                <div className="task-builder-row">
                    <FunctionTray functions={this.props.functions} />
                    <div className="task-builder-diagram-wrapper"
                        onDrop={e => this.DropFunction(e)}
                        onDragOver={e => { e.preventDefault(); }}
                    >
                        <Toast ref="toast" />
                        <SRD.DiagramWidget className="task-builder-diagram"
                            ref="diagram"
                            diagramEngine={this.engine}
                            maxNumberPointsPerLink="0" />
                    </div>
                </div>
            </div>
        );
    }
}