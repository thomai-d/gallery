import { Link, useNavigation } from "react-router-dom";
import { GallerySummary } from "../../../../model/api/GallerySummary";
import { ThreeDots } from "react-loader-spinner";
import classes from './gallery-preview.component.module.scss';

export interface GalleryPreviewProps {
  gallery: GallerySummary;
}

export const GalleryPreview = ({ gallery }: GalleryPreviewProps) => {
  const link = `/gallery/${gallery.id}`;

  const navi = useNavigation();

  const isLoading = navi.state === 'loading' && navi.location.pathname === link;

  return (
    <div key={gallery.id} className={classes.root}>
      <Link to={link}>
        {isLoading && <ThreeDots wrapperClass={classes.loader} />}
        <img className={isLoading ? classes.loading : ''} src={gallery.previewImageUrl} alt={gallery.id} />
        <span>{gallery.id}</span>
      </Link>
    </div >
  )
}