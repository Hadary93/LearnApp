import './App.css';
import WordsPage from './Components/WordPage/WordPage';
import TopMenu from './Components/TopMenu/TopMenu';
import ArticlePage from './Components/ArticlePage/ArticlePage';
import { Video } from './Components/VideoPage/Video';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { AppProvider } from './Components/AppContext';
import PracticePage from './Components/Practice/PracticePage';
import Footer from './Components/Footer/Footer';
import Home from './Components/AddContentPage/Home';
import Words from './Components/Practice/Words/Words';
import Sentences from './Components/Practice/Sentences/Sentences';
import Analytics from './Components/Analytics/Analytics';

function App() {
  const a = 0;
  return (
    <AppProvider>
      <div style={{display:"flex", flexDirection:"column", marginTop:"3%", bottom:0 , position:"relative"}}>
        <BrowserRouter>
          {/* Top Menu component */}
          <TopMenu />

          {/* Define Routes for rendering components */}
          <Routes>
            <Route path="/" element={<Home />} />
            <Route path="/video" element={<Video />} />
            <Route path="/word" element={<WordsPage />} />
            <Route path="/article" element={<ArticlePage />} />
            <Route path="/practice" element={<PracticePage />} />
            <Route path="/practice/words" element={<Words />} />
            <Route path="/practice/sentences" element={<Sentences />} />
            <Route path="/analytics" element={<Analytics />} />
          </Routes>
          <Footer />
        </BrowserRouter>
    
      </div>
    </AppProvider>

  )
}

export default App
