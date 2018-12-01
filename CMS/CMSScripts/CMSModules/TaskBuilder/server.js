// All JavaScript in here will be loaded server-side
// Expose components globally so ReactJS.NET can use them
//import Components from './components';

//global.Components = Components;

// React server-side rendering
require('expose-loader?React!react');
require('expose-loader?ReactDOM!react-dom');
require('expose-loader?ReactDOMServer!react-dom/server');

require('expose-loader?Components!./Components/Demo-Serialize.jsx');