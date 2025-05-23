document.addEventListener('DOMContentLoaded', () => {
    const imageScroller = document.querySelector('.image-scroller'); // 獲取輪播外層容器
    const imageWrapper = imageScroller.querySelector('.image-wrapper'); // 獲取圖片 wrapper
    const scrollerImages = imageWrapper.querySelectorAll('img');
    let enlargedImage = null; // 用來追蹤當前被放大的圖片

    // 點擊事件監聽器
    imageWrapper.addEventListener('click', (event) => {
        const clickedImage = event.target;

        // 確保點擊的是圖片
        if (clickedImage.tagName === 'IMG') {
            if (enlargedImage === clickedImage) {
                // 如果點擊的是已經放大的圖片，則縮小並恢復輪播
                clickedImage.classList.remove('enlarged');
                enlargedImage = null;
                imageWrapper.classList.remove('paused'); // 恢復輪播
                // 恢復 scroller 的 overflow，以便在圖片縮小時隱藏超出部分
                imageScroller.style.overflow = 'hidden'; 
            } else {
                // 如果有其他圖片已經放大，先縮小它
                if (enlargedImage) {
                    enlargedImage.classList.remove('enlarged');
                }
                
                // 放大被點擊的圖片並暫停輪播
                clickedImage.classList.add('enlarged');
                enlargedImage = clickedImage;
                imageWrapper.classList.add('paused'); // 暫停輪播

                // 臨時改變 scroller 的 overflow，讓放大的圖片不被裁剪
                // 但這可能導致輪播區域外的內容也顯現，若需完整lightbox效果會更複雜
                imageScroller.style.overflow = 'visible'; 
            }
        }
    });

    // 滑鼠移入輪播區域時暫停輪播
    imageScroller.addEventListener('mouseenter', () => {
        if (!enlargedImage) { // 只有在沒有圖片放大的情況下才響應鼠標移入
            imageWrapper.classList.add('paused');
        }
    });

    // 滑鼠移出輪播區域時恢復輪播
    imageScroller.addEventListener('mouseleave', () => {
        if (!enlargedImage) { // 只有在沒有圖片放大的情況下才響應鼠標移出
            imageWrapper.classList.remove('paused');
        }
    });


    // 點擊輪播區域外，恢復輪播和縮小圖片
    document.addEventListener('click', (event) => {
        // 如果點擊的目標不在 imageScroller 內部，且有圖片被放大
        if (!imageScroller.contains(event.target) && enlargedImage) {
            enlargedImage.classList.remove('enlarged');
            enlargedImage = null;
            imageWrapper.classList.remove('paused'); // 恢復輪播
            imageScroller.style.overflow = 'hidden'; // 恢復 scroller 的 overflow
        }
    });
});