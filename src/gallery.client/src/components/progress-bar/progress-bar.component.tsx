import classes from './progress-bar.component.module.scss';

interface ProgressBarProps {
  progress: number;
  total: number;
  text?: string;
}

/**
 * Renders a simple progress bar with a text and a progress indicator.
 */
export const Progressbar = ({ text, progress, total }: ProgressBarProps) => {
  return (
    <div className={classes.root}>
      {text && <span>{text}</span>}
      <progress value={progress} max={total}></progress>
    </div>
  )
}