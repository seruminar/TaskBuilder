import * as _ from "lodash";
import { DiagramModel } from "storm-react-diagrams";
import { taskBuilderDataSource } from "../../taskBuilder/TaskBuilderDataSource";
import { Graph } from "../../models/graph/Graph";
import { ITaskGraphModel } from "../../models/ITaskGraphModel";
import { TaskGraphMode } from "../../models/graph/TaskGraphMode";
import { FunctionModel } from "./FunctionModel";
import { GraphEngine } from "../GraphEngine";

export class GraphModel extends DiagramModel {
    constructor(graph: ITaskGraphModel, engine: GraphEngine) {
        super();

        this.deSerializeDiagram(graph.graph, engine);

        this.setLocked(graph.mode === TaskGraphMode.readonly);

        this.gridSize = 10;
    }

    deSerializeDiagram(other: Graph, diagramEngine: GraphEngine) {
        this.deSerialize(other, diagramEngine);

        // deserialize nodes
        _.forEach(other.nodes, node => {
            const nodeOb = diagramEngine.getNodeFactory(node.type).getNewInstance({ node }) as FunctionModel;
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

    serializeGraph() {
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