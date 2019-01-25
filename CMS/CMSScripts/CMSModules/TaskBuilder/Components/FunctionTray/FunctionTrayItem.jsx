class FunctionTrayItem extends React.Component {
    render() {
        const typeIdentifier = this.props.functionModel.typeIdentifier;

        return (
            <div
                draggable className="task-builder-tray-item"
                onDragStart={e => {
                    e.dataTransfer.setData("functionSignature", typeIdentifier);
                }}
            >
                <i className="icon-w-products-data-source" />
                <span>{this.props.functionModel.displayName}</span>
            </div>
        );
    }
}