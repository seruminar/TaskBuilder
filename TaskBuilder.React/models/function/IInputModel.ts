import { IParameterModel } from "./IParameterModel";
import { InputType } from "./InputType";
import { IInputValueModel } from "./inputValue/IInputValueModel";

export interface IInputModel extends IParameterModel {
    inlineOnly: boolean;
    inputType: InputType;
    structureModel: IInputValueModel;
    filledModel: IInputValueModel;
}