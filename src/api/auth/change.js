'use strict'
import Router from 'koa-router';
import bodyParser from 'koa-bodyparser';
import { getRequest } from '../../controllers/auth_controller/change.js';

const router = new Router();
router.use(bodyParser());

router.post('/api/auth/change', async (ctx)=>{
    await getRequest(ctx.body.request,ctx);
});

export default router;

