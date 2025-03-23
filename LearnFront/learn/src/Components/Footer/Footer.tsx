import './Footer.css';
import { MdEmail } from 'react-icons/md';
import { FaLinkedin } from 'react-icons/fa';

export default function Footer() {
  return (
    <div className='footer'>
      <hr className="separator"></hr>
      <div style={{ display: "flex", flexDirection: "column", fontFamily:"bold"}}>
        <div style={{margin:10}}>
          <MdEmail style={{scale:1.5, color:"var(--theme-color)"}}/>
          <a href='mailto:k7adaryd@gmail.com' target="_blank" rel="noopener noreferrer">K7adaryd@gmail.com</a>
        </div>
        <div style={{margin:10}}>
          <FaLinkedin style={{scale:1.5, color:"var(--theme-color)"}}/>
          <a href='https://www.linkedin.com/in/khaled-elhadary-646909127' target="_blank" rel="noopener noreferrer" >Khaled Elhadary</a>
        </div>
      
      </div>
    </div>
  );
}