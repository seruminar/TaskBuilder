class FunctionTrayItem extends React.Component {
    render() {
        const signature = this.props.functionModel.assembly + this.props.functionModel.typeName;

        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("functionSignature", signature);
                }}
            >
                <i className="icon-w-products-data-source" />
                <span>{this.props.functionModel.displayName}</span>
            </div>
        );
    }
}