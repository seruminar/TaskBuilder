var path = require('path');

module.exports = {
	mode: 'development',
	entry: './CMSScripts/CMSModules/TaskBuilder/server.js',
	output: {
		path: path.resolve(__dirname, 'CMSScripts/CMSModules/TaskBuilder'),
		filename: 'TaskBuilderComponents.js'
	},
	module: {
		rules: [
				{
					test: /\.jsx?$/,
					exclude: /node_modules/,
					use: {
						loader: 'babel-loader'
					}
				}
		]
	}
};