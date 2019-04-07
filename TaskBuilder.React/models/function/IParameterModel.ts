import { INamedPortModel } from "./INamedPortModel";

export interface IParameterModel extends INamedPortModel {
    typeNames: string[];
    displayColor: string;
}