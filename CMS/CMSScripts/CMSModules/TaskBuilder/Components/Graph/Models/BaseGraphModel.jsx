class BaseGraphModel extends SRD.DiagramModel {
    constructor(graph, engine) {
        super();

        this.deSerializeDiagram(graph.graph, engine);

        this.setLocked(graph.graphMode === "readonly");

        this.gridSize = 10;
    }

    deSerializeDiagram(other, diagramEngine) {
        this.deSerialize(other, diagramEngine);

        // deserialize nodes
        _.forEach(other.nodes, node => {
            const nodeOb = diagramEngine.getNodeFactory(node.type).getNewInstance(node);
            nodeOb.setParent(this);
            nodeOb.deSerialize(node, diagramEngine);

            // attach input values
            _.forEach(nodeOb.getInputs(), i => {
                const inputValue = other.inputValues[i.getID()];

                if (inputValue) {
                    taskBuilderDataSource.setInputValue(i.getID(), inputValue);
                }
            });

            this.addNode(nodeOb);
        });

        // deserialze links
        _.forEach(other.links, link => {
            const linkOb = diagramEngine.getLinkFactory(link.type).getNewInstance();
            linkOb.setParent(this);
            linkOb.deSerialize(link, diagramEngine);
            this.addLink(linkOb);
        });
    }

    serializeDiagram() {
        return _.merge(this.serialize(), {
            links: _.map(this.links, link => {
                return link.serialize();
            }),
            nodes: _.map(this.nodes, node => {
                return node.serialize();
            }),
            inputValues: taskBuilderDataSource.graph.graph.inputValues
        });
    }
}