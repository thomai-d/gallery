import { Outlet } from 'react-router-dom';
import { GalleryPreview } from './components/gallery-preview/gallery-preview.component';
import classes from './gallery-overview.page.module.scss'
import { useGalleries } from '../../hooks/use-galleries';
import { ThreeDots } from 'react-loader-spinner';
import { COLORS } from '../../utils/colors';

/**
 * Page that renders a gallery overview page.
 */
export const GalleryOverviewPage = () => {

  const galleries = useGalleries()

  if (galleries.status === 'error') {
    throw new Error((galleries.error as { message: string }).message)
  }

  if (galleries.status !== 'success') {
    return <ThreeDots color={COLORS.primaryLight} />
  }

  return (
    <div className={classes['gallery-container']}>
      {galleries.data!.map(gallery => <GalleryPreview key={gallery.id} gallery={gallery} />)}
      <Outlet />
    </div>
  )
};