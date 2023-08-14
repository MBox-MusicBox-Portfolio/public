import nodemailer from 'nodemailer';
import ejs from 'ejs';
import fs from 'fs';
import IMessage from "../../models/IMessage";


export async function sendNewMail(msg: IMessage): Promise<boolean> {
    try {
        const transporter = nodemailer.createTransport({
            host: process.env.MAIL_HOST,
            port: parseInt(process.env.MAIL_PORT ?? '465'),
            secure: true,
            auth: {
                user: process.env.MAIL_USER,
                pass: process.env.MAIL_PASSWORD
            }
        });

        const layoutTemplate = await fs.promises.readFile(__dirname + '/../../views/layout.ejs', 'utf8');
        const template = await fs.promises.readFile(__dirname + `/../../views/${msg.Template}.ejs`, 'utf8');
        const body = ejs.render(template, msg);
        const html = ejs.render(layoutTemplate, {
            title: 'Music Box Email',
            body: body
        });
        const mailOptions = {
            from: process.env.MAIL_USER,
            to: msg.Email,
            subject: 'Music Box',
            html: html
        };
        try {
            await transporter.sendMail(mailOptions);
            console.log('Електронний лист надіслано.');
        } catch (err) {
            console.error('Помилка при відправці електронного листа:', err);
            throw err;
        }
        return true;
    } catch (err) {
        console.log(err)
        return false;
    }
}
