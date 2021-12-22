import React, { useState } from 'react';
import Content from './Content';
import LeftNav from './LeftNav';
import LowerButtons from './LowerButtons';

import '../styles/LeftNav.css';

export let WRAPPER_REF;

function Layout() {
    WRAPPER_REF = React.createRef();

    return (
        <div ref={WRAPPER_REF} className="wrapper is-nav-open">
            <div className="p-2 bd-highlight left-nav-fixed" style={{ "width": "10%" }}>
                <LeftNav />
                <LowerButtons />
            </div>
            <Content />
        </div>
    );
}

export default Layout;
