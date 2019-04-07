import { NodeModel } from "storm-react-diagrams";
import * as _ from "lodash";

import { taskBuilderDataSource } from "../../taskBuilder/TaskBuilderDataSource";
import { IFunctionModel } from "../../models/function/IFunctionModel";
import { InvokeModel } from "./port/InvokeModel";
import { DispatchModel } from "./port/DispatchModel";
import { IInvokeModel } from "../../models/function/IInvokeModel";
import { IDispatchModel } from "../../models/function/IDispatchModel";
import { InputModel } from "./port/InputModel";
import { IInputModel } from "../../models/function/IInputModel";
import { OutputModel } from "./port/OutputModel";
import { IOutputModel } from "../../models/function/IOutputModel";
import { GraphEngine } from "../GraphEngine";
import { BasePortModel } from "./port/BasePortModel";
import { NodeType } from "../../models/function/NodeType";

export class FunctionModel extends NodeModel {
    functionTypeGuid = "";
    ports: {
        [s: string]: BasePortModel;
    };

    getFunction(): IFunctionModel {
        return taskBuilderDataSource
            .getFunctionByTypeGuid(this.functionTypeGuid);
    }

    getInvoke(): BasePortModel<IInvokeModel> {
        const found = _.find(this.ports, p => p.type === "invoke");

        if (found) {
            return found as BasePortModel<IInvokeModel>;
        }

        throw new Error("Invoke not found!");
    }

    getDispatchs(): BasePortModel<IDispatchModel>[] {
        return _.filter(this.ports, p => p.type === "dispatch") as BasePortModel<IDispatchModel>[];
    }

    getInputs(): BasePortModel<IInputModel>[] {
        return _.filter(this.ports, p => p.type === "input") as BasePortModel<IInputModel>[];
    }

    getOutputs(): BasePortModel<IOutputModel>[] {
        return _.filter(this.ports, p => p.type === "output") as BasePortModel<IOutputModel>[];
    }

    constructor(func: IFunctionModel, locationPoint?: { x: number, y: number }) {
        super(nameof<NodeType.function>());

        this.functionTypeGuid = func.typeGuid;

        if (locationPoint) {
            this.addInvoke(func.invoke);
            func.dispatchs.map(d => this.addDispatch(d));
            func.inputs.map(i => this.addInput(i));
            func.outputs.map(o => this.addOutput(o));

            this.x = locationPoint.x;
            this.y = locationPoint.y;
        }
    }

    private addInvoke(model: IInvokeModel) {
        return super.addPort(new InvokeModel(model.name));
    }

    private addDispatch(model: IDispatchModel) {
        return super.addPort(new DispatchModel(model.name));
    }

    private addInput(model: IInputModel) {
        const port = super.addPort(new InputModel(model.name));

        if (model.inlineOnly) {
            port.setLocked(true);
        }

        return port;
    }

    private addOutput(model: IOutputModel) {
        const port = super.addPort(new OutputModel(model.name));

        return port;
    }

    deSerialize(other: any, engine: GraphEngine) {
        super.deSerialize(other, engine);
        this.functionTypeGuid = other.functionTypeGuid;
    }

    serialize() {
        return _.merge(super.serialize(), {
            functionTypeGuid: this.functionTypeGuid
        });
    }

    setLocked(locked: boolean) {
        super.setLocked(locked);

        _.forEach(this.ports, port => {
            _.forEach(port.getLinks(), link => {
                link.setLocked(locked);
            });
        });
    }
}