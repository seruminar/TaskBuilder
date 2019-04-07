import * as React from "react";

import { TasksControllerResult } from "./TasksControllerResult";

export interface IDiagramToastState {
    show: boolean,
    result: TasksControllerResult,
    message: string
}

export class DiagramToast extends React.Component<{}, IDiagramToastState> {
    state: IDiagramToastState = {
        show: false,
        result: TasksControllerResult.success,
        message: ""
    }

    private timeout: NodeJS.Timeout;

    componentDidUpdate() {
        if (this.state.show) {
            clearTimeout(this.timeout);

            this.timeout = setTimeout(() => {
                this.setState({
                    show: false,
                    result: TasksControllerResult.success,
                    message: ""
                });
            }, 3000);
        }
    }

    render() {
        const display = this.state.show ? { display: "block" } : { display: "none" };

        let alertClass = "";

        switch (this.state.result) {
            case TasksControllerResult.success:
                alertClass = "alert-success";
                break;
            case TasksControllerResult.error:
                alertClass = "alert-error";
                break;
        }

        return (
            <div
                className={`task-builder-toast alert ${alertClass}`}
                style={display}
            >
                <span className="alert-icon">
                    <i className="icon-check-circle" />
                    <span className="sr-only">
                        Success
                    </span>
                </span>
                <span className="alert-label">
                    {this.state.message}
                </span>
            </div>
        );
    }
}