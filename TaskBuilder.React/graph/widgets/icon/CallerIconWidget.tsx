import * as React from "react";

import { BaseIconWidget, IconWidgetProps } from "./BaseIconWidget";
import { IPortModel } from "../../../models/function/IPortModel";

export class CallerIconWidget extends BaseIconWidget<IPortModel> {
    constructor(props: IconWidgetProps<IPortModel>) {
        super("port-icon", props);
    }

    getClassName() {
        return "port " + super.getClassName() + (this.state.selected ? this.bem("-selected") : "");
    }

    select = (select: boolean) => {
        this.setState({ selected: select });
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
            <div
                {...this.getProps()}
                onMouseEnter={() => this.select(true)}
                onMouseLeave={() => this.select(false)}
                data-name={this.props.port.name}
                data-nodeid={this.props.port.parentModel.getID()}
            >
                <i className={this.getIcon()} />
            </div>
        );
    }
}