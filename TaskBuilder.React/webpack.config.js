/// <binding BeforeBuild='Run - Development' AfterBuild='Run - Production' />
const path = require("path");
const extractTextPlugin = require("extract-text-webpack-plugin");
const optimizeCssAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const tsNameof = require("ts-nameof");

const isProduction = process.env.NODE_ENV === "production";

const makeSourceMaps = isProduction ? false : "cheap-module-eval-source-map";
const getDevSuffix = isProduction ? "" : "Dev";

const jsFileName = `taskBuilder${getDevSuffix}.js`;
const cssFileName = `taskBuilder${getDevSuffix}.css`;

module.exports = {
    mode: process.env.NODE_ENV,
    devtool: makeSourceMaps,
    entry: "./index",
    output: {
        path: path.resolve(__dirname, "../CMS/CMSScripts/CMSModules/TaskBuilder"),
        filename: jsFileName
    },
    resolve: {
        extensions: [".ts", ".tsx", ".js", ".json"]
    },
    module: {
        rules: [
            {
                test: /\.scss$/,
                use: extractTextPlugin.extract({
                    fallback: "style-loader",
                    use: ["css-loader", "sass-loader"]
                })
            },
            {
                test: /\.tsx?$/,
                use: [
                    {
                        loader: "awesome-typescript-loader",
                        options: {
                            getCustomTransformers: () => ({ before: [tsNameof] })
                        }
                    }
                ]
            }
        ]
    },
    plugins: [
        new extractTextPlugin(cssFileName),
        new optimizeCssAssetsPlugin({
            assetNameRegExp: /^((?!Dev).)*\.css/
        })
    ],
    watch: !isProduction,
    watchOptions: {
        ignored: /node_modules/
    }
};