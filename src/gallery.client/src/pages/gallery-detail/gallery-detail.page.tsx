import { useParams } from "react-router-dom"
import { useGallery } from "../../hooks/use-gallery"
import { ImageSlider } from "./components/image-slider/image-slider.component"
import { Progressbar } from "../../components/progress-bar/progress-bar.component"

import classes from './gallery-detail.page.module.scss'

/**
 * Page that renders a gallery.
 */
export const GalleryDetailPage = () => {

  const { id } = useParams<{ id: string }>()

  const gallery = useGallery(id!)

  if (gallery.error) {
    throw new Error(gallery.error.message)
  }

  if (gallery.isLoading) {
    return <Progressbar text="Loading..." progress={gallery.loaded} total={gallery.total} />
  }

  return (
    <div className={classes.root} id="detail-root">
      <h1>{id}</h1>
      <ImageSlider imageUrls={gallery.gallery!.imageUrls} />
    </div>
  )
}