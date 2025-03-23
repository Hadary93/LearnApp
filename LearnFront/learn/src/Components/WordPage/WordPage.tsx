import { useEffect, useState } from 'react';
import './WordPage.css';
import {  List, Switch, Table } from 'antd';
import type { TableColumnsType, TableProps } from 'antd';
import { ISentence, IWord } from '../Interfaces/Interfaces';
import { GetAllWords, GetSentences } from '../EndPoints/Text';
import MediaPanel from '../Custom/MediaPanel';

export default function WordsPage() {
  const [words, setWords] = useState<IWord[] | undefined>([]);
  const [sentences, setSentences] = useState<ISentence[] | undefined>([]);
  const [sentenceHash, setSentenceHash] = useState<string | null>(null);

  useEffect(() => {
    async function fetchWord() {
      var words = await GetAllWords();
      words?.map((x, i) => x.key = i);
      setWords(words);
      console.log(words)
    }
    fetchWord();
  },[])

  const columns: TableColumnsType<IWord> = [
    { title: 'German', dataIndex: 'wordText' },
    { title: 'English', dataIndex: 'translation' },
    { title: 'Favourite', dataIndex: 'favourite',
      filters: [
        {
          text: 'favourited',
          value: true,
        }
        
      ],
      onFilter: (value, record) => record.favourite === value ,
      render: (favourite: boolean) => (
        <Switch 
          checked={favourite} 
          onChange={(checked) => console.log('Toggled:', checked)}
        />
      )
    },
      
  ];

  const handleRowClick = (record: IWord) => {
    async function fetchSentences() {
      var sentences = await GetSentences(record.wordText);
      sentences?.map((x, i) => x["key"] = i);
      setSentences(sentences);
    }
    fetchSentences();
  };
  const onChange: TableProps<IWord>['onChange'] = (pagination, filters, sorter, extra) => {
    console.log('params', pagination, filters, sorter, extra);
  };

  return (
    <div className='word-page'>
      <div className='word-page-table'>
          <Table<IWord> columns={columns} dataSource={words} onChange={onChange}  pagination={false}  showSorterTooltip={{ target: 'sorter-icon' }} onRow={(record) => ({
            onClick: () => handleRowClick(record), // Call function on row click
          })} />
      </div>


      <div className='word-page-sentences'>
        {sentences && sentences?.length > 0 && <List
          itemLayout="horizontal"
          dataSource={sentences}
          renderItem={(item, index) => (
            <List.Item>
              <List.Item.Meta key={index}
                title={<p onClick={()=>{setSentenceHash(item.hash??'')}}>{item.words?.map(x => x.wordText).join(" ")}</p>}
                description={item.words?.map(x => x.wordText).join(" ")}
              />
            </List.Item>
          )}
        />}
      </div>       
      <MediaPanel sentenceHash={sentenceHash} selectedWord={''}/>
    </div>
  );
}

