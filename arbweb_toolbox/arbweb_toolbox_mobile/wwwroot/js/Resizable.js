function set_resizable() {
    interact('.resizable')
        .resizable({
            edges: { top: true, left: true, bottom: true, right: true },
            listeners: {
                move: function (event) {
                    let { x, y } = event.target.dataset;

                    x = (parseFloat(x) || 0) + event.deltaRect.left;
                    y = (parseFloat(y) || 0) + event.deltaRect.top;

                    Object.assign(event.target.style, {
                        width: `${event.rect.width}px`,
                        height: `${event.rect.height}px`,
                        transform: `translate(${x}px, ${y}px)`
                    });

                    Object.assign(event.target.dataset, { x, y });
                }
            }
        });
}

function get_viewport()
{
    const vw = Math.max(document.documentElement.clientWidth || 0, window.innerWidth || 0)
    const vh = Math.max(document.documentElement.clientHeight || 0, window.innerHeight || 0)

    var l_vpr =
    {
        width: vw,
        height: vh
    };

    return l_vpr;
}

function get_coordinates()
{
    let l_img = document.getElementById('ocr_img').getBoundingClientRect();
    let l_box = document.getElementById('ocr_box').getBoundingClientRect();

    var l_crd =
    {
        g_img: l_img,
        g_box: l_box
    };

    return l_crd;
}