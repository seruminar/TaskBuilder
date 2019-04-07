import * as React from "react";
import * as ReactDOM from "react-dom";

import { TaskBuilder } from "./taskBuilder/TaskBuilder";

require('./styles/style.scss');
require("storm-react-diagrams/src/sass/main.scss");

// @ts-ignore
global.React = React;
// @ts-ignore
global.ReactDOM = ReactDOM;
// @ts-ignore
global.TaskBuilder = TaskBuilder;