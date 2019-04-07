import * as React from "react";
import * as _ from "lodash";

import { ITaskGraphModel } from "../models/ITaskGraphModel";
import { ITaskModelsModel } from "../models/ITaskModelsModel";

import { TaskDiagram } from "./TaskDiagram";

export interface ITaskBuilderProps {
    models: ITaskModelsModel,
    graph: ITaskGraphModel,
    endpoints: { [endpoint: string]: string },
    secureToken: string
}

export interface ITaskBuilderState {
    hasError: boolean,
    error: Error | null,
    errorInfo: React.ErrorInfo | null
}

export class TaskBuilder extends React.Component<ITaskBuilderProps, ITaskBuilderState> {
    state: ITaskBuilderState = {
        hasError: false,
        error: null,
        errorInfo: null
    };

    componentDidCatch(error: Error, errorInfo: React.ErrorInfo) {
        // Display fallback UI
        this.setState({
            hasError: true,
            error: error,
            errorInfo: errorInfo
        });
    }

    render() {
        console.log(this.props);

        const { hasError, error, errorInfo } = this.state;

        if (hasError && error && errorInfo) {

            console.log(this.state);

            return (
                <div style={{ padding: '10px 15px' }}>
                    <h2>
                        Something went wrong.
                        </h2>
                    <div style={{ whiteSpace: 'pre-wrap' }}>
                        {error.stack}
                        <br />
                        {errorInfo.componentStack}
                    </div>
                </div>
            );
        }

        // Hack to revert version of _ that was loaded by Kentico's cmsrequire
        const setUnderscore = setInterval(() => {
            if (_.VERSION === "1.5.2") {
                _.noConflict();
                clearInterval(setUnderscore);
            }
        }, 50);

        return <TaskDiagram {...this.props} />;
    }
}