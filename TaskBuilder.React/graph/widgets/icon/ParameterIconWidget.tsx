import * as React from "react";

import { BaseIconWidget, IconWidgetProps } from "./BaseIconWidget";
import { IParameterModel } from "../../../models/function/IParameterModel";

export class ParameterIconWidget extends BaseIconWidget<IParameterModel> {
    constructor(props: IconWidgetProps<IParameterModel>) {
        super("port", props);
    }

    getClassName() {
        return super.getClassName() + (this.state.selected ? this.bem("-selected") : "");
    }

    select = (select: boolean) => {
        if (!this.props.locked) {
            this.setState({ selected: select });
        }
    }

    getIcon() {
        if (this.state.selected) {
            return "icon-chevron-right-circle";
        }

        if (this.props.port.isLinked()) {
            return "icon-caret-right";
        }

        return "icon-chevron-right";
    }

    render() {
        return (
            <div className="port-icon">
                <div
                    {...this.getProps()}
                    onMouseEnter={() => this.select(true)}
                    onMouseLeave={() => this.select(false)}
                    data-name={this.props.port.name}
                    data-nodeid={this.props.port.parentModel.getID()}
                    style={{ color: this.props.port.model.displayColor }}
                >
                    <i
                        className={this.getIcon()}
                        style={{ opacity: this.props.locked ? 0 : 1 }}
                    />
                </div>
            </div>
        );
    }
}