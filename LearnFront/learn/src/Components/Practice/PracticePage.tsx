import { Card } from 'antd';
import './PracticePage.css';
import Meta from 'antd/es/card/Meta';
import matchingImage from './matching.png';
import puzzleImage from './jigsaw.png';
import { useNavigate } from "react-router-dom";

interface PracticeItem {
    name: string;
    desc: string;
    src: string;
  }

export default function PracticePage() {
    const navigate = useNavigate();

    const practices = [{ "name": 'Words', "src": matchingImage, "desc":"Match words with translation" }, 
        { "name": 'Sentences', "src": puzzleImage,"desc":"Construct sentences" }]
    
        const onCardClick = (item: PracticeItem): void => {
            console.log("Clicked:", item);
            navigate(`/practice/${encodeURIComponent(item.name)}`, { state: item });
            // Hier kannst du weitere Aktionen ausführen, z. B. eine Navigation
          };
    return (
        <div className='practice-page'>
            {practices.map((x) => <Card
                key={x.name}
                hoverable
                style={{ width: 240, margin: 10 }}
                cover={<img alt="example" src={x.src} />}
                onClick={()=>onCardClick(x)}>
                <Meta title={x.name} description={x.desc} />
            </Card>)}
        </div>
    );
}

