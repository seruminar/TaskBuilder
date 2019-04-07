import * as React from "react";
import * as _ from "lodash";

import { AbstractNodeFactory, DiagramEngine, NodeModel } from "storm-react-diagrams";
import { FunctionModel } from "../models/FunctionModel";
import { FunctionWidget } from "../widgets/FunctionWidget";
import { taskBuilderDataSource } from "../../taskBuilder/TaskBuilderDataSource";
import { graphEngine } from "../../taskBuilder/TaskDiagram";
import { InputType } from "../../models/function/InputType";
import { NodeType } from "../../models/function/NodeType";

export class FunctionFactory extends AbstractNodeFactory {
    constructor() {
        super(nameof<NodeType.function>());
    }

    generateReactWidget(_diagramEngine: DiagramEngine, node: NodeModel): JSX.Element {
        return <FunctionWidget node={node as FunctionModel} />;
    }

    getNewInstance(initialConfig: { node: FunctionModel, typeGuid?: string, mousePoint?: { x: number, y: number } }): NodeModel {
        const typeGuid = initialConfig.typeGuid || initialConfig.node.functionTypeGuid;

        const func = taskBuilderDataSource.getFunctionByTypeGuid(typeGuid);

        const nodeModel = new FunctionModel(func, initialConfig.mousePoint);
        nodeModel.setParent(graphEngine.getDiagramModel());

        _.forEach(nodeModel.getInputs(), i => {
            switch (i.model.inputType) {
                case InputType.structureOnly:
                case InputType.filled:
                    taskBuilderDataSource.setInputValue(i.getID(), i.model.filledModel);
            }
        });

        return nodeModel;
    }
}