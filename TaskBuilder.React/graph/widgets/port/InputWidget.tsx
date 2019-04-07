import * as React from "react";

import { BasePortWidget, PortWidgetProps } from "./BasePortWidget";
import { IInputModel } from "../../../models/function/IInputModel";
import { InputValueWidget } from "../InputValueWidget";
import { ParameterIconWidget } from "../icon/ParameterIconWidget";

export class InputWidget extends BasePortWidget<IInputModel> {
    constructor(props: PortWidgetProps<IInputModel>) {
        super("port-input", props);
    }

    render() {
        let valueWidget;

        if (!this.props.port.isLinked()) {
            valueWidget = <InputValueWidget {...this.props} />;
        }

        return (
            <div {...this.getProps()} >
                <ParameterIconWidget {...this.props} locked={this.props.port.model.inlineOnly} />
                <div className="port-name">
                    {this.props.port.model.displayName}
                    {valueWidget}
                </div>
            </div>
        );
    }
}