const SRD = window["storm-react-diagrams"];

class BaseGraphModel extends SRD.DiagramModel {
    constructor(graph, graphMode, engine) {
        super();

        this.deSerializeDiagram(JSON.parse(graph), engine);

        this.setLocked(graphMode === "readonly");

        this.setGridSize(10);
        this.setZoomLevel(100);
    }
}