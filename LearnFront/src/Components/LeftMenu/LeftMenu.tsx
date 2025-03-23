import './LeftMenu.css';
import { useEffect } from 'react';
import { AppContext, useAppContext } from "../AppContext";
import { GetVideosNames } from '../EndPoints/Video';
import { GetAllArticlesNames } from '../EndPoints/Text';


export default function LeftMenu() {

  const { leftMenuSelected, setLeftMenuSelected } = useAppContext(AppContext);
  const { leftMenuContent, setLeftMenuContent } = useAppContext(AppContext);
  const { mainMenuSelected } = useAppContext(AppContext);

  useEffect(() => {
    const fetchLeftMenuOptions = async () => {
      switch (mainMenuSelected) {
        case "Videos":
          setLeftMenuContent(await GetVideosNames());
          break;
        case "Words":
          //setLeftMenuContent(await GetWordsGroups());
          break;
        case "Articles":
          setLeftMenuContent(await GetAllArticlesNames());
          break;
        default:
          setLeftMenuContent([])
          break;
      }
    }
    fetchLeftMenuOptions();
  }, [mainMenuSelected, setLeftMenuSelected, setLeftMenuContent]);

  return (
    <div className='left-menu'>
      {leftMenuContent?.map(x => <button key={x} className={leftMenuSelected === x ? 'menu-item-selected' : "menu-item"} onClick={() => setLeftMenuSelected(leftMenuSelected === x ? undefined : x)}>{x}</button>)}
    </div>
  );
}