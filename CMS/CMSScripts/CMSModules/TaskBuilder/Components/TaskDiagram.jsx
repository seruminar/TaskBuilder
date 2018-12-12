var SRD = window["storm-react-diagrams"];

class TaskDiagram extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            serialized: null
        };

        engine = new SRD.DiagramEngine();

        // Temporarily needed for default links and ports
        engine.installDefaultFactories();

        // Register factories from initialization code
        // Foreach function
        engine.registerNodeFactory(new BaseNodeFactory("startNode"));
        engine.registerNodeFactory(new BaseNodeFactory("eventlogNode"));

        // Deserialize from database
        //var model2 = new DiagramModel();
        //model2.deSerializeDiagram(JSON.parse(str), engine);
        //engine.setDiagramModel(model2);

        // TEST ------------- Create a new model for testing

        // Create a start node
        var node1 = new BaseNodeModel("startNode");
        var port1 = node1.addOutPort("Out");
        node1.setPosition(100, 100);

        // Create another node
        var node2 = new BaseNodeModel("eventlogNode");
        var port2 = node2.addInPort("In");
        node2.setPosition(400, 100);

        // Link the 2 nodes together
        var link1 = port1.link(port2);

        // Add to diagram model
        var model = new SRD.DiagramModel();
        model.addAll(node1, node2, link1);

        //5) load model into engine
        engine.setDiagramModel(model);

        // TEST ------------- Create a new model for testing






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