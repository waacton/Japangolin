import * as React from "react";

const AddEventButton = (props) => {
    return <div>
        <button onClick={props.whenClicked}>{props.name}</button>
    </div>;
}

export default AddEventButton