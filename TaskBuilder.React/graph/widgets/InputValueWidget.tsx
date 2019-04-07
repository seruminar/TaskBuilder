import * as React from "react";

import { BaseWidget } from "storm-react-diagrams";
import { PortWidgetProps } from "./port/BasePortWidget";
import { IInputModel } from "../../models/function/IInputModel";
import { taskBuilderDataSource } from "../../taskBuilder/TaskBuilderDataSource";
import { InputType } from "../../models/function/InputType";
import { IFieldModel } from "../../models/function/inputValue/IFieldModel";
import { FieldType } from "../../models/function/inputValue/FieldType";

export class InputValueWidget extends BaseWidget<PortWidgetProps<IInputModel>> {
    constructor(props: PortWidgetProps<IInputModel>) {
        super("port-value", props);
    }

    portID = this.props.port.getID();

    setValue = (key: string, value: string | null) => {
        if (value !== "" && value !== null) {
            taskBuilderDataSource.getInputValue(this.portID).fields[key].value = [value];
            this.forceUpdate();
        }
    }

    setFunctionLocked = (locked: boolean) => {
        this.props.port.getParent().setLocked(locked);
    }

    renderField = (key: string, field: IFieldModel) => {
        const filledValue = taskBuilderDataSource.getInputValue(this.portID).fields[key].value[0] || "";

        switch (field.type) {
            case FieldType.text:
                return (
                    <input
                        type="text"
                        key={key}
                        value={filledValue}
                        onFocus={() => this.setFunctionLocked(true)}
                        onInput={() => this.setFunctionLocked(true)}
                        onBlur={() => this.setFunctionLocked(false)}
                        onChange={e => this.setValue(key, e.target.value)}
                    />
                );
            case FieldType.dropdown:
                return (
                    <select
                        key={key}
                        value={filledValue}
                        onChange={e => this.setValue(key, e.target.selectedOptions[0].textContent)}
                    >
                        {field.value.map((value, index) =>
                            (
                                <option
                                    key={index}
                                    value={value}
                                >
                                    {value}
                                </option>
                            )
                        )}
                    </select>
                );
            case FieldType.bool:
                return (
                    <input
                        type="checkbox"
                        key={key}
                        value={filledValue}
                        onFocus={() => this.setFunctionLocked(true)}
                        onInput={() => this.setFunctionLocked(true)}
                        onBlur={() => this.setFunctionLocked(false)}
                        onChange={e => this.setValue(key, e.target.value)}
                    />
                );
            case FieldType.int:
                return (
                    <input
                        type="number"
                        key={key}
                        value={filledValue}
                        onFocus={() => this.setFunctionLocked(true)}
                        onInput={() => this.setFunctionLocked(true)}
                        onBlur={() => this.setFunctionLocked(false)}
                        onChange={e => this.setValue(key, e.target.value)}
                    />
                );
            case FieldType.float:
                return (
                    <input
                        type="number"
                        step="any"
                        key={key}
                        value={filledValue}
                        onFocus={() => this.setFunctionLocked(true)}
                        onInput={() => this.setFunctionLocked(true)}
                        onBlur={() => this.setFunctionLocked(false)}
                        onChange={e => this.setValue(key, e.target.value)}
                    />
                );
        }
    }

    render() {
        switch (this.props.port.model.inputType) {
            case InputType.structureOnly:
            case InputType.filled:
                const fields = this.props.port.model.structureModel.fields;

                return (
                    <div {...this.getProps()}>
                        {Object.keys(fields).map(key => this.renderField(key, fields[key]))}
                    </div>
                );
            default:
                return null;
        }
    }
}