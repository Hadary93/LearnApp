import './TopMenu.css';
import React, { useState } from 'react';
import type { MenuProps } from 'antd';
import { Button, Menu, Modal } from 'antd';
import { LuWholeWord } from 'react-icons/lu';
import { SiFuturelearn } from 'react-icons/si';
import { Link } from 'react-router-dom';
import { FiHome } from 'react-icons/fi';
import { TbPresentationAnalytics } from 'react-icons/tb';
import { RxVideo } from 'react-icons/rx';
import { PiArticleNyTimesLight } from 'react-icons/pi';
import Login from '../Login/Login';
import { AppContext, useAppContext } from '../AppContext';

type MenuItem = Required<MenuProps>['items'][number];

const items: MenuItem[] = [
    {
        key: 'Home',
        icon: <FiHome />,
        label: (
            <Link to="/">
                Home
            </Link>
        ),
    },
    {
        key: 'Video',
        icon: <RxVideo />,
        label: (
            <Link to="/video">
                Video
            </Link>
        ),
    },
    {
        key: 'Word',
        icon: <LuWholeWord />,
        label: (
            <Link to="/word">
                Word
            </Link>
        ),
    },
    {
        key: 'Article',
        icon: <PiArticleNyTimesLight />,
        label: (
            <Link to="/article">
                Article
            </Link>
        ),
    },
    {
        key: 'Practice',
        icon: <SiFuturelearn />,
        label: (
            <Link to="/practice">
                Practice
            </Link>
        ),
    },
    {
        key: 'Analytics',
        icon: <TbPresentationAnalytics />,
        label: (
            <Link to="/analytics">
                Analytics
            </Link>
        ),
    },
];

const TopMenu: React.FC<{}> = () => {
    const [current, setCurrent] = useState('Video');
    const { isLoginOpen, setIsLoginOpen } = useAppContext(AppContext);

    const showModal = () => {
        setIsLoginOpen(true);
    };
    const closeMoal = () => {
        setIsLoginOpen(false);
    };

    const onClick: MenuProps['onClick'] = (e) => {
        console.log('click ', e);
        setCurrent(e.key);
    };
    return (
        <div className='top-menu'>
            <Menu onClick={onClick} selectedKeys={[current]} mode="horizontal" items={items} />
            <Button type="primary" onClick={showModal}>
                Login
            </Button>
            <Modal title="Login" open={isLoginOpen} onCancel={closeMoal}>
                <Login></Login>
            </Modal>
        </div>
    );
}

export default TopMenu;

