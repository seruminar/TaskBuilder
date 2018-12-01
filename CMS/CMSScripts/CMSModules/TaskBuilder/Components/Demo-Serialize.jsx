//var SRD = require('../Vendor/main');
var SRD = window["storm-react-diagrams"];

class StartModel extends SRD.NodeModel {
    constructor() {
        super("startNode");
    }

    addInPort(label) {
        return this.addPort(new SRD.DefaultPortModel(true, SRD.Toolkit.UID(), label));
    }

    addOutPort(label) {
        return this.addPort(new SRD.DefaultPortModel(false, SRD.Toolkit.UID(), label));
    }

    getInPorts() {
        return _.filter(this.ports, portModel => {
            return portModel.in;
        });
    }

    getOutPorts() {
        return _.filter(this.ports, portModel => {
            return !portModel.in;
        });
    }
}

class StartFactory extends SRD.AbstractNodeFactory {
    constructor() {
        super("startNode");
    }

    generateReactWidget(diagramEngine, node) {
        return <SRD.DefaultNodeWidget node={node} />;
    }

    getNewInstance() {
        return new StartModel();
    }
}

class EventLogNodeModel extends SRD.NodeModel {
    constructor() {
        super("eventLogNode");
    }

    addInPort(label) {
        return this.addPort(new SRD.DefaultPortModel(true, SRD.Toolkit.UID(), label));
    }

    addOutPort(label) {
        return this.addPort(new SRD.DefaultPortModel(false, SRD.Toolkit.UID(), label));
    }

    getInPorts() {
        return _.filter(this.ports, portModel => {
            return portModel.in;
        });
    }

    getOutPorts() {
        return _.filter(this.ports, portModel => {
            return !portModel.in;
        });
    }
}

class EventLogFactory extends SRD.AbstractNodeFactory {
    constructor() {
        super("eventLogNode");
    }

    generateReactWidget(diagramEngine, node) {
        return <SRD.DefaultNodeWidget node={node} />;
    }

    getNewInstance() {
        return new StartNodeModel();
    }
}

class ErrorWrapper extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            hasError: false,
            error: null,
            errorInfo: null
        };
    }


    componentDidCatch(error, info) {
        // Display fallback UI
        this.setState({
            hasError: true,
            error: error,
            errorInfo: info
        });
    }

    render() {

        if (this.state.hasError) {
            // You can render any custom fallback UI
            return (
                <div>
                    <h2>Something went wrong.</h2>
                    <div style={{ whiteSpace: 'pre-wrap' }}>
                        {this.state.error && this.state.error.toString()}
                        <br />
                        {this.state.errorInfo.componentStack}
                    </div>
                </div>
            );
        }

        return (
            <DemoSerialize/>
            );
    }
}

class DemoSerialize extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            diagram: "initial message",
            serialized: null,
            hasError: false,
            error: null,
            errorInfo: null
        };

        //1) setup the diagram engine
        engine = new SRD.DiagramEngine();
        engine.installDefaultFactories();
        engine.registerNodeFactory(new StartFactory());
        engine.registerNodeFactory(new EventLogFactory());

        //2) setup the diagram model
        var model = new SRD.DiagramModel();

        //3-A) create a default node
        var node1 = new StartModel("Node 1", "rgb(0,192,255)");
        var port1 = node1.addOutPort("Out");
        node1.setPosition(100, 100);

        //3-B) create another default node
        var node2 = new EventLogNodeModel("Node 2", "rgb(192,255,0)");
        var port2 = node2.addInPort("In");
        node2.setPosition(400, 100);

        //3-C) link the 2 nodes together
        var link1 = port1.link(port2);

        //4) add the models to the root graph
        model.addAll(node1, node2, link1);

        //5) load model into engine
        engine.setDiagramModel(model);

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
            body: JSON.stringify(this.engine.diagramModel.serializeDiagram()),
        });
    }



    render() {
        return (
            <div>
                <button onClick={e => this.SerializeGraph(e)}>
                    Serialize Graph!
				</button>
                <button onClick={e => this.RunGraph(e)}>
                    Run Graph!
				</button>
                <span>{this.state.serialized}</span>
                <SRD.DiagramWidget className="srd-demo-canvas" diagramEngine={this.engine} />
                <div ref="diagram">{this.state.diagram}</div>
            </div>
        );
    }
}