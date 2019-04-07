import { TaskGraphMode } from "./graph/TaskGraphMode";
import { Graph } from "./graph/Graph";

export interface ITaskGraphModel {
    graph: Graph;
    mode: TaskGraphMode;
}