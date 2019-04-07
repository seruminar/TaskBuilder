import { IInvokeModel } from "./IInvokeModel";
import { IDispatchModel } from "./IDispatchModel";
import { IInputModel } from "./IInputModel";
import { IOutputModel } from "./IOutputModel";

export interface IFunctionModel {
    typeGuid: string;
    displayName: string;
    displayColor: string;
    invoke: IInvokeModel;
    dispatchs: IDispatchModel[];
    inputs: IInputModel[];
    outputs: IOutputModel[];
}