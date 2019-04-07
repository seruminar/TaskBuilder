import { DiagramEngine } from "storm-react-diagrams";
import { GraphModel } from "./models/GraphModel";

export class GraphEngine extends DiagramEngine {
    get graphModel() {
        return this.diagramModel as GraphModel;
    }
}