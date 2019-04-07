import * as React from "react";

export interface IDiagramButtonProps {
    onClick: (event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => void;
    iconClass: string;
    text: string;
}

export class DiagramButton extends React.Component<IDiagramButtonProps> {
    render() {
        return (
            <button
                type="button"
                onClick={this.props.onClick}>
                <i className={`cms-icon-80 ${this.props.iconClass}`} />
                <br />
                <span>{this.props.text}</span>
            </button>
        );
    }
}