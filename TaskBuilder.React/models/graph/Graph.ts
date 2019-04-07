import { DiagramModel } from "storm-react-diagrams";

import { IInputValueModel } from "../function/inputValue/IInputValueModel";

export class Graph extends DiagramModel {
    inputValues : { [inputValue: string]: IInputValueModel }
}