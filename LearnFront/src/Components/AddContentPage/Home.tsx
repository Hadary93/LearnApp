import React, { useState } from 'react';
import { InboxOutlined } from '@ant-design/icons';
import type { UploadProps } from 'antd';
import { Button, message, Upload } from 'antd';
import { contentUploadAPI, ProcessContent } from '../EndPoints/Content';
import "./Home.css"

export default function Home() {
  let [filesNames, setFilesNames] = useState<string[]>([]);
  let [processState, setProcessState] = useState<string>('Process');

  const onProcessincClick: React.MouseEventHandler<HTMLElement> = async () => {
    console.log(filesNames)
    setProcessState('Processing')

    await (async () => {
      await ProcessContent(filesNames);
    })();
    setProcessState('Process')
  }
  const { Dragger } = Upload;

  const props: UploadProps = {
    name: 'file',
    multiple: true,
    action: contentUploadAPI,
    onChange(info) {
      const { status } = info.file;
      if (status === "uploading") {
      } else {
      }
      if (status === 'done') {
        setFilesNames(() => info.fileList.map(x => x.originFileObj?.name??""))
        message.success(`${info.file.name} file uploaded successfully.`);
      } else if (status === 'error') {
        message.error(`${info.file.name} file upload failed.`);
      }
    },
    onDrop(e) {
      console.log('Dropped files', e.dataTransfer.files);
    },
  };

  return (
    <div className='home'>
      <Dragger {...props}>
        <div style={{ display: 'flex', flexDirection: "column" }}>
          <p className="ant-upload-drag-icon">
            <InboxOutlined />
          </p>
          <p className="ant-upload-text">Click or drag file to this area to upload</p>
          <p className="ant-upload-hint">
            .mp4 videos and vtt files are only allowed
          </p>
        </div>
      </Dragger>
          <Button type="primary" onClick={onProcessincClick}>
        {processState}
      </Button>

    </div>
  )
}

