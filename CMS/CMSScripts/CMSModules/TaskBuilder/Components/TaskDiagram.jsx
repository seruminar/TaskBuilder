var SRD = window["storm-react-diagrams"];

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
        var graphModel = new SRD.DiagramModel();
        graphModel.deSerializeDiagram(JSON.parse(this.props.graph), engine);

        engine.setDiagramModel(graphModel);

        this.engine = engine;
    }

    SerializeGraph(diagram, e) {
        console.log(JSON.stringify(diagram.engine.diagramModel.serializeDiagram()));
    }

    RunGraph(diagram, e) {
        fetch("/Kentico11_hf_TaskBuilder/taskbuilder/RunTask", {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            body: JSON.stringify(diagram.engine.diagramModel.serializeDiagram())
        });
    }

    DropFunction(e) {
        var functionModel = JSON.parse(e.dataTransfer.getData("functionModel"));

        var nodeFactory = this.engine.getNodeFactory(functionModel.Name);

        var node = nodeFactory.getNewInstance(null, true);

        var points = this.engine.getRelativeMousePoint(e);

        node.x = points.x;
        node.y = points.y;

        this.engine
            .getDiagramModel()
            .addNode(node);
        this.forceUpdate();
    }

    render() {
        return (
            <div className="task-builder-diagram-wrapper"

                onDrop={e => this.DropFunction(e)}

                onDragOver={e => { e.preventDefault(); }}
            >
                <SRD.DiagramWidget className="task-builder-diagram" diagramEngine={this.engine} />
            </div>
        );
    }
}