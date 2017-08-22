import * as React from "react";
import { Link, NavLink} from "react-router-dom";

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className="main-nav">
                <div className="navbar navbar-inverse">
                <div className="navbar-header">
                    <button type="button" className="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                        <span className="sr-only">Toggle navigation</span>
                        <span className="icon-bar" />
                        <span className="icon-bar" />
                        <span className="icon-bar" />
                    </button>
                    <Link className='navbar-brand' to={ '/' }>React Demo</Link>
                </div>
                <div className="clearfix" />
                <div className="navbar-collapse collapse">
                    <ul className="nav navbar-nav">
                        <li>
                            <NavLink exact to={"/"} activeClassName="active">
                                <span className="glyphicon glyphicon-home" /> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/counter"} activeClassName="active">
                                <span className="glyphicon glyphicon-education" /> Counter
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/fetchdata"} activeClassName="active">
                                <span className="glyphicon glyphicon-th-list" /> Fetch data
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/calendar"} activeClassName="active">
                                <span className="glyphicon glyphicon-calendar" /> Calendar-Demo
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={"/japanese"} activeClassName="active">
                                <span className="glyphicon glyphicon-yen" /> Japanese
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
