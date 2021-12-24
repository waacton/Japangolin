import { useEffect, useState } from "react";

type HelloWorldProps = {
    value: string;
};

function HelloWorld(props: HelloWorldProps) {
    const [state, setState] = useState("[n/a]");
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        loadData()
    }, [])

    const loadData = async () => {
        setLoading(true);
        const response = await fetch('/weatherforecast');
        const data = await response.json();
        setState(data[0].summary)
        setLoading(false);
    }

    return (
        <div>
            <h1>Hello, World!</h1>
            <h3>Prop: {props.value}</h3>
            <h3>State: {state}</h3>
            <button onClick={() => loadData()} disabled={loading}>Update</button>
        </div>
    )
}

export default HelloWorld