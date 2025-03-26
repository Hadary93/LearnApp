import { Button, Checkbox, Form, FormProps, Input, message } from 'antd';
import './Login.css';
import { AppContext, useAppContext } from '../AppContext';
import { DeepLAPIKey } from '../EndPoints/Translate';

type FieldType = {
    username?: string;
    password?: string;
    remember?: string;
};

const onFinishFailed: FormProps<FieldType>['onFinishFailed'] = (errorInfo) => {
    console.log('Failed:', errorInfo);

};

export default function Login() {
    const { setIsLoginOpen } = useAppContext(AppContext);
    const [messageApi,contextHolder] = message.useMessage();

    const onFinish: FormProps<FieldType>['onFinish'] = (values) => {
        console.log('Success:', values);
        const activateDeepL = async () => {
            const success = await DeepLAPIKey(values.password ?? '');
            if (success != undefined && success) {
                setIsLoginOpen(false);
                const success = () => {
                    messageApi.open({
                        type: 'success',
                        content: 'DeepL activated',
                    });
                };
                success();
            } else {
                const error = () => {
                    messageApi.open({
                        type: 'error',
                        content: 'Something went wrong',
                    });
                };
                error();
            }
        }

        activateDeepL();
    };
    return (
        <>
        <Form
            name="basic"
            labelCol={{ span: 8 }}
            wrapperCol={{ span: 16 }}
            style={{ maxWidth: 600 }}
            initialValues={{ remember: true }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
        >
            <Form.Item<FieldType>
                label="E-mail"
                name="username"
                rules={[{ required: true, message: 'Please input your username!' }]}
            >
                <Input />
            </Form.Item>

            <Form.Item<FieldType>
                label="DeepL API key"
                name="password"
                rules={[{ required: true, message: 'Please input your password!' }]}
            >
                <Input.Password />
            </Form.Item>

            <Form.Item<FieldType> name="remember" valuePropName="checked" label={null}>
                <Checkbox>Remember me</Checkbox>
            </Form.Item>

            <Form.Item label={null}>
                <Button type="primary" htmlType="submit">
                    Submit
                </Button>
            </Form.Item>
        </Form>
        {contextHolder}
        </>
    );
}

