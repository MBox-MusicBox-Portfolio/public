import dotenv from 'dotenv';
import { createClient } from 'redis';
dotenv.config();

const redis = createClient({
     url: process.env.CONNECTION_URI_REDIS,
  });

redis.on('error', () => {
    console.error(`[${new Date().toLocaleString()}]   : Redis module:[Service] Connect refused check available service`);
});
 redis.on("connect",() =>{
      console.log(`[${new Date().toLocaleString()}]   : Redis module:[Service] Connection successfull`);
 }) 

await redis.connect();

/**
 * Создаёт ключ и значение ключа
 * Create new redis key and set key-value too  
 * @param {*} key 
 * @param {*} object 
 */
export async function RedisSetValue(key,object)
{
    try {
        await redis.set(key,object,'EX',3600);
    } catch (error) {
        console.error(`[${new Date().toLocaleString()}] : Redis module: [Service Exceptions]: + ${error}`);
        throw error;
    } 
}
/**
 * An current function checking key 
 * Проверяет существует ключ или нет
 * @param {*} key 
 */
export async function RedisGetKey(key)
{
    try{
        if(key)
        {
          const keyRedis= await redis.exists(key);
          console.log(keyRedis);
        }else{
            console.error(`[${new Date().toLocaleString()}] : Redis module: [RedisGetKey()]: Empty params `);
        }
    }catch(ex){

    }
}
/**
 * An current function removed key 
 * Удаляет ключ
 * @param {*} key 
 * @returns 
 */
export async function RedisDelKey(key)
{
    if(key)
    {
      return (await redis.del(key));
    }else{
        console.error(`[${new Date().toLocaleString()}] : Redis module: [RedisDelKey()]: Empty params `);
    }
}

/**
 * Get key value
 * Получает значение ключа
 * @param {*} key 
 * @returns 
 */

export async function RedisGetValue(key)
{
    try{
        return (await redis.get(key));    
    }catch(ex){
        console.error(`[${new Date().toLocaleString()}] : Redis module: [RedisGetValue()]: ${ex}`);
    }
}
