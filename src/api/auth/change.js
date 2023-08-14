'use strict'
import Router from 'koa-router';
import bodyParser from 'koa-bodyparser';
import { changePassword } from '../../controllers/auth_controller/change.js';

const router = new Router();
router.use(bodyParser());

router.post('/api/auth/change', async (ctx)=>{
    const newPassword = await changePassword(ctx.query,ctx);
   // ctx.body = JSON.parse(newPassword);
   ctx.body = "Change password route";
});

export default router;

