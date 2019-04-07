import { BaseWidget } from "storm-react-diagrams";
import { PortWidgetProps } from "../port/BasePortWidget";

export interface IconWidgetProps<TModel> extends PortWidgetProps<TModel> {
    locked?: boolean;
}

export interface IconWidgetState {
    selected: boolean;
}

export class BaseIconWidget<TModel> extends BaseWidget<IconWidgetProps<TModel>, IconWidgetState> {
    state: IconWidgetState = {
        selected: false
    };
}
