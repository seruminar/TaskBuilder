var SRD = window["storm-react-diagrams"];

class TaskDiagram extends React.Component {
    engine;

    constructor(props) {
        super(props);

        this.state = {
            serialized: null
        };

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

    SerializeGraph(e) {
        e.preventDefault();

        this.setState({
            serialized: JSON.stringify(this.engine.diagramModel.serializeDiagram())
        });
    }

    RunGraph(e) {
        e.preventDefault();

        fetch("/Kentico11_hf_TaskBuilder/taskbuilder/RunTask", {
            method: "POST",
            cache: "no-cache",
            headers: {
                "Content-Type": "application/json; charset=utf-8"
            },
            body: JSON.stringify(this.engine.diagramModel.serializeDiagram())
        });
    }

    render() {
        return (
            <div className="task-builder-diagram-wrapper">
                <button onClick={e => this.SerializeGraph(e)}>
                    Serialize Graph!
				</button>
                <button onClick={e => this.RunGraph(e)}>
                    Run Graph!
				</button>
                <span>{this.state.serialized}</span>
                <SRD.DiagramWidget className="task-builder-diagram" diagramEngine={this.engine} />
            </div>
        );
    }
}