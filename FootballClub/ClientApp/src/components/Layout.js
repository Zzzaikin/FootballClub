import React, { useState } from 'react';
import Content from './Content';
import LeftNav from './LeftNav';
import LowerButtons from './LowerButtons';
import Section from './Section';

export let WRAPPER_REF;

function Layout() {

    const [wrapperRef, setWrapperRef] = useState(React.createRef());
    WRAPPER_REF = wrapperRef;

    return (
        <div ref={WRAPPER_REF} className="wrapper">
            <div className="p-2 bd-highlight" style={{ "width": "10%" }}>
                <LeftNav />
                <LowerButtons />
            </div>
            <Content />
        </div>
    );
}

export default Layout;
