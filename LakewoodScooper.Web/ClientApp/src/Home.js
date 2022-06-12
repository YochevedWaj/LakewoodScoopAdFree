import React, { useEffect, useState } from 'react';
import axios from 'axios';

const Home = () => {
    const [posts, setPosts] = useState([]);

    useEffect(() => {

        const getPosts = async () => {
            const { data } = await axios.get('/api/tls/scrape');
            setPosts(data);
        }
        getPosts();
    }, []);

    return (
        <div className='container mt-5'>
            <div className='row'>
                <a href='https://www.thelakewoodscoop.com' target='_blank'>
                    <img src='https://www.thelakewoodscoop.com/news/wp-includes/images/thelakewoodscoop_logo.png' />
                </a>
                <h1>  Lakewood Scoop Ad Free!!!!</h1>
            </div>
            <div className='row mt-3'>
                <div className='col-md-12'>
                    <table className='table table-hover table-striped table-bordered'>
                        <thead>
                            <tr>
                                <th>Title</th>
                                <th>Image</th>
                                <th>Text</th>
                                <th>Comments Count</th>
                            </tr>
                        </thead>
                        <tbody>
                            {posts.map(({ link, title, imageUrl, text, commentsCount }, i) => {
                                return <tr key={i}>
                                    <td>
                                        <a href={link} target='_blank'>{title}</a>
                                    </td>
                                    <td>
                                        {imageUrl && <img src={imageUrl} />}
                                        {!imageUrl && <h1>No picture</h1>}
                                    </td>
                                    <td>
                                        {text && `${text}...`}
                                        {!text && 'Click on the title to read about this'}
                                    </td>
                                    <td>{commentsCount}</td>
                                </tr>
                            })}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    )
}

export default Home;