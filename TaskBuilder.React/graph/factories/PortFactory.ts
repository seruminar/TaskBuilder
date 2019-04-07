import { AbstractPortFactory } from "storm-react-diagrams";
import { InvokeModel } from "../models/port/InvokeModel";
import { DispatchModel } from "../models/port/DispatchModel";
import { InputModel } from "../models/port/InputModel";
import { OutputModel } from "../models/port/OutputModel";
import { PortType } from "../../models/function/PortType";

export class PortFactory extends AbstractPortFactory {
    getNewInstance() {
        switch (this.type) {
            case nameof<PortType.invoke>():
                return new InvokeModel();
            case nameof<PortType.dispatch>():
                return new DispatchModel();
            case nameof<PortType.input>():
                return new InputModel();
            case nameof<PortType.output>():
                return new OutputModel();
        }

        return null as any;
    }
}